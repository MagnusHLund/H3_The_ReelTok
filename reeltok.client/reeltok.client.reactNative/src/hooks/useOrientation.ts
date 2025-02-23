import { DeviceMotion, DeviceMotionMeasurement } from 'expo-sensors'
import { useState, useEffect } from 'react'
import { Platform } from 'react-native'

type Orientation = 'up' | 'down' | 'left' | 'right'

// Due to the app following a TikTok style, the app is mostly fixed in portrait mode.
// To simplify our styling, we locked the app to be only portrait, within app.json.
// Because of this, we are unable to use expo-screen-orientation, as it relies on the app not being locked in app.json.
// Instead we listen to the device's gyroscope and determine the orientation ourselves.
// This solution does not work well on the emulator, but it works perfectly fine on an actual device.
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

const UseOrientation = (): Orientation => {
  const [orientation, setOrientation] = useState<Orientation>('up')

  useEffect(() => {
    if (Platform.OS == 'web') {
      // TODO: Add orientation support for web?
      return
    }

    const subscription = DeviceMotion.addListener((data: DeviceMotionMeasurement) => {
      const newOrientation = calculateOrientation(data.rotation)
      if (newOrientation !== undefined) {
        setOrientation(newOrientation)
      }
    })

    DeviceMotion.setUpdateInterval(1000)

    return () => {
      subscription.remove()
    }
  }, [])

  return orientation
}

export default UseOrientation
