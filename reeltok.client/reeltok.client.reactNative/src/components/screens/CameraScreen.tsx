import { View } from 'react-native'
import React from 'react'
import CameraSelected from '../Layout/common/CameraSelected'
import Navbar from '../Layout/common/Navbar'

const CameraScreen = () => {
  return (
    <View>
      <CameraSelected />
      <Navbar />
    </View>
  )
}

export default CameraScreen
