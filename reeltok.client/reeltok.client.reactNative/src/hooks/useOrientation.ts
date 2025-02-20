import { DeviceMotionSensor } from './../../node_modules/expo-sensors/src/DeviceMotion';
import { useState, useEffect } from 'react'
import { Dimensions } from 'react-native'

const useOrientation = () => {
  const [orientation, setOrientation] = useState('portrait')

  DeviceMotionSensor

  return orientation
}

export default useOrientation
