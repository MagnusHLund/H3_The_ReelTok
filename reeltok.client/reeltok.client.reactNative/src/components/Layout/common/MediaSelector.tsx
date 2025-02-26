import { StyleSheet, View } from 'react-native'
import React, { useState } from 'react'
import CustomButton from '../../input/CustomButton'
import Entypo from '@expo/vector-icons/Entypo';
import MaterialIcons from '@expo/vector-icons/MaterialIcons';
import CameraSelected from './CameraSelected';
import pickImage from '../../../utils/imagePicker';
export default function MediaSelector() {
  const [showCamera, setShowCamera] = useState(false);

  const handlePickImage = async () => {
    try {
      const selectedImage = await pickImage();

    } catch (error) {
      console.error(error.message);
    }
  };

  return (
    <>
      {showCamera && <CameraSelected />}
      <View style={styles.container}>
        <CustomButton widthPercentage={0.45} onPress={() => {
          console.log('test');
          setShowCamera(!showCamera);
        }}><Entypo name="camera" size={24} color="white" /></CustomButton>
        <CustomButton widthPercentage={0.45} onPress={() => handlePickImage()}><MaterialIcons name="photo-library" size={24} color="white" /></CustomButton>
      </View>
    </>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    top: -40,
    backgroundColor: '#262626',
    width: '100%',
    paddingTop: 10,
    borderRadius: 5,
    marginBottom: 5,
    paddingLeft: 5,
    paddingRight: 5,
    paddingBottom: 15,
  },
})
