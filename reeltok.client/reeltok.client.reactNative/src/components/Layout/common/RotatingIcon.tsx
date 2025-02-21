import React, { useEffect, useRef } from 'react'
import { Animated } from 'react-native'
import Ionicons from '@expo/vector-icons/Ionicons'
import useOrientation from '../../../hooks/useOrientation'

type RotatingIconProps = {
  name: string
  size: number
  color: string
}

const RotatingIcon: React.FC<RotatingIconProps> = ({ name, size, color }) => {
  const iconRotation = useRef(new Animated.Value(0)).current
  const orientation = useOrientation()

  useEffect(() => {
    let rotationValue: number
    switch (orientation) {
      case 'left':
        rotationValue = 1 // 90deg
        break
      case 'right':
        rotationValue = -1 // -90deg
        break
      default:
        rotationValue = 0 // 0deg
    }

    Animated.timing(iconRotation, {
      toValue: rotationValue,
      duration: 300,
      useNativeDriver: true,
    }).start()
  }, [orientation])

  const rotationInterpolation = iconRotation.interpolate({
    inputRange: [-1, 0, 1],
    outputRange: ['-90deg', '0deg', '90deg'],
  })

  return (
    <Animated.View style={{ transform: [{ rotate: rotationInterpolation }] }}>
      <Ionicons name={name} size={size} color={color} />
    </Animated.View>
  )
}

export default RotatingIcon
