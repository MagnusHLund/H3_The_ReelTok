import MaterialIcons from '@expo/vector-icons/MaterialIcons'
import pickImage from '../../../utils/imagePickerUtils'
import CustomButton from '../../input/CustomButton'
import { StyleSheet, View } from 'react-native'
import Entypo from '@expo/vector-icons/Entypo'
import CameraSelected from './CameraSelected'
import React, { useState } from 'react'

const MediaSelector: React.FC = () => {
  const [showCamera, setShowCamera] = useState(false)

  const handlePickImage = async () => {
    try {
      const selectedImage = await pickImage()
    } catch (error) {
      console.error(error.message)
    }
  }

  return (
    <View style={styles.outerContainer}>
      {showCamera && <CameraSelected />}
      <View style={styles.innerContainer}>
        <CustomButton
          widthPercentage={0.45}
          onPress={() => {
            setShowCamera(!showCamera)
          }}
        >
          <Entypo name="camera" size={24} color="white" />
        </CustomButton>
        <CustomButton widthPercentage={0.45} onPress={handlePickImage}>
          <MaterialIcons name="photo-library" size={24} color="white" />
        </CustomButton>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  outerContainer: {
    backgroundColor: 'black',
    width: '100%',
  },
  innerContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: '#262626',
    width: '100%',
    borderRadius: 10,
    paddingLeft: 5,
    paddingRight: 5,
    paddingTop: 10,
    paddingBottom: 15,
    borderColor: '#ff0050',
    borderWidth: 1,
  },
})

export default MediaSelector
