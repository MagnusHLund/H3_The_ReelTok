import { View, useWindowDimensions, StyleSheet } from 'react-native'
import React from 'react'
import Camera from '../Layout/common/Camera'

const CameraScreen = () => {
  const { width, height } = useWindowDimensions()
  return (
    <View style={styles.container}>
      <Camera cameraMode="video" />
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    height: '100%',
    width: '100%',
  },
})

export default CameraScreen
