import { useState, useEffect } from 'react'
import { DeviceMotion, DeviceMotionMeasurement } from 'expo-sensors'

type Orientation = 'up' | 'down' | 'left' | 'right'

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
