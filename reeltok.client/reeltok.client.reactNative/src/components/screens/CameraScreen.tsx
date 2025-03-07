import { View, StyleSheet } from 'react-native'
import React from 'react'
import Camera from '../LayoutTemp/camera/Camera'

const CameraScreen = () => {
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
