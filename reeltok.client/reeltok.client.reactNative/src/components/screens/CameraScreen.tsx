import { View, useWindowDimensions } from 'react-native'
import React from 'react'
import Camera from '../Layout/common/Camera'

const CameraScreen = () => {
  const { width, height } = useWindowDimensions()
  return (
    <View style={{ height: '100%', width: '100%' }}>
      <Camera cameraMode="video" />
    </View>
  )
}

export default CameraScreen
