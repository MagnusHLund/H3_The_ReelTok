import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import useAppDimensions from '../../../hooks/useAppDimensions'
import MaterialIcons from '@expo/vector-icons/MaterialIcons'
import mediaPicker from '../../../utils/mediaPickerUtils'
import { useNavigation } from '@react-navigation/native'
import CustomButton from '../../input/CustomButton'
import { StyleSheet, View } from 'react-native'
import Entypo from '@expo/vector-icons/Entypo'
import Gradient from './GradientBackground'
import React, { useState } from 'react'

const MediaSelector: React.FC = () => {
  const [selectedMedia, setSelectedMedia] = useState<string>()
  const { navbarHeight } = useAppDimensions()
  const navigation = useNavigation<NativeStackNavigationProp<any>>()

  const handlePickMedia = async () => {
    try {
      const media = await mediaPicker()
      if (media) {
        setSelectedMedia(media)
        navigation.navigate('UploadVideo', { video: media })
      }
    } catch (error) {
      throw new Error(error.message)
    }
  }

  return (
    <View style={[styles.outerContainer, { paddingBottom: navbarHeight }]}>
      <Gradient colors={['transparent', 'transparent', 'black', 'black']}>
        <View style={styles.innerContainer}>
          <CustomButton
            widthPercentage={0.45}
            onPress={() => {
              navigation.navigate('Camera')
            }}
          >
            <Entypo name="camera" size={24} color="white" />
          </CustomButton>
          <CustomButton widthPercentage={0.45} onPress={handlePickMedia}>
            <MaterialIcons name="photo-library" size={24} color="white" />
          </CustomButton>
        </View>
      </Gradient>
    </View>
  )
}

const styles = StyleSheet.create({
  outerContainer: {
    backgroundColor: 'transparent',
    width: '100%',
    position: 'absolute',
    paddingBottom: 10,
    bottom: 0,
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
