import { StyleSheet, View } from 'react-native'
import CustomButton from '../../input/CustomButton'
import Entypo from '@expo/vector-icons/Entypo'
import MaterialIcons from '@expo/vector-icons/MaterialIcons'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { useNavigation } from '@react-navigation/native'
import Gradient from './GradientBackground'
import useAppDimensions from '../../../hooks/useAppDimensions'
import pickImage from '../../../utils/imagePickerUtils'

const MediaSelector: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()
  const { navbarHeight } = useAppDimensions()

  const handlePickImage = async () => {
    try {
      // TODO: @Jorjo6712 the selectedImage is unused.
      const selectedImage = await pickImage()
    } catch (error) {
      console.error(error.message)
    }
  }

  return (
    <View style={[styles.outerContainer, { paddingBottom: navbarHeight }]}>
      <Gradient colors={['transparent', 'transparent', 'black', 'black']}>
        <View style={styles.innerContainer}>
          <CustomButton
            widthPercentage={0.45}
            onPress={() => {
              navigation.replace('Camera')
            }}
          >
            <Entypo name="camera" size={24} color="white" />
          </CustomButton>
          <CustomButton widthPercentage={0.45} onPress={handlePickImage}>
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
