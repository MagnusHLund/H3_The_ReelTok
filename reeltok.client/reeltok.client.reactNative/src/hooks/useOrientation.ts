import { DeviceMotion, DeviceMotionMeasurement } from 'expo-sensors'
import * as ScreenOrientation from 'expo-screen-orientation'
import { useFocusEffect } from '@react-navigation/native'
import { useState, useCallback } from 'react'

export type Orientation = 'up' | 'down' | 'left' | 'right'

const calculateOrientation = (
  rotation: DeviceMotionMeasurement['rotation']
): Orientation | undefined => {
  const radiansToDegrees = (radians: number) => radians * (180 / Math.PI)
  const deadZone = 25

  const { beta, gamma } = rotation
  const betaDegrees = radiansToDegrees(beta)
  const gammaDegrees = radiansToDegrees(gamma)

  const isInDeadZone = (degrees: number) => degrees > -deadZone && degrees < deadZone

  if (isInDeadZone(betaDegrees)) {
    if (!isInDeadZone(gammaDegrees)) {
      return gammaDegrees > deadZone ? 'right' : 'left'
    }
  } else if (!isInDeadZone(betaDegrees) && isInDeadZone(gammaDegrees)) {
    return betaDegrees > deadZone ? 'up' : 'down'
  }
  return undefined
}

const allowedScreens = ['VideoFeed']
const allowedComponents = ['Comments', 'VideoOverlay', 'RotatingIcon']

const useOrientation = (currentRoute: string, component: string): Orientation => {
  const [orientation, setOrientation] = useState<Orientation>('up')

  useFocusEffect(
    useCallback(() => {
      let subscription: { remove: () => void } | null = null

      const adjustOrientation = async () => {
        if (allowedScreens.includes(currentRoute) || allowedComponents.includes(component)) {
          await ScreenOrientation.unlockAsync()
          subscription = DeviceMotion.addListener((data: DeviceMotionMeasurement) => {
            const newOrientation = calculateOrientation(data.rotation)
            if (newOrientation !== undefined) {
              setOrientation(newOrientation)
            }
          })
          DeviceMotion.setUpdateInterval(1000)
        } else {
          await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.PORTRAIT_UP)
        }
      }

      adjustOrientation()

      return () => {
        subscription?.remove()
        ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.PORTRAIT_UP)
      }
    }, [currentRoute])
  )

  return orientation
}

export default useOrientation
