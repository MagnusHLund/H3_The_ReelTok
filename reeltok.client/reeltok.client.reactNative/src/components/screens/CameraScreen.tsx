import { View, useWindowDimensions } from 'react-native'
import React from 'react'
import Camera from '../Layout/common/Camera'

const CameraScreen = () => {
  const { width, height } = useWindowDimensions()
  return (
    <View style={{ height: height, width: width }}>
      <Camera />
    </View>
  )
}

export default CameraScreen
