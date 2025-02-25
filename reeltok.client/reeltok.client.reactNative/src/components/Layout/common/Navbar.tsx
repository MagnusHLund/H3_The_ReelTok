import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { useNavigation } from '@react-navigation/native'
import CustomButton from '../../input/CustomButton'
import { View, StyleSheet } from 'react-native'
import { ScreenName } from '../../../Router'
import MediaSelector from './MediaSelector'
import RotatingIcon from './RotatingIcon'
import React, { useState } from 'react'

const Navbar: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()
  const [displayMediaSelector, setDisplayMediaSelector] = useState(false)

  const toggleMediaSelectorForVideoUpload = () => {
    setDisplayMediaSelector(!displayMediaSelector)
  }

  const handleNavigation = (screenName: ScreenName) => {
    navigation.replace(screenName)
  }

  return (
    <>
      {displayMediaSelector && <MediaSelector />}
      <View style={styles.container}>
        <CustomButton transparent={true} onPress={() => handleNavigation('VideoFeed')}>
          <RotatingIcon name="play" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={toggleMediaSelectorForVideoUpload}>
          <RotatingIcon name="add" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={() => handleNavigation('Profile')}>
          <RotatingIcon name="person-circle-sharp" size={32} color="white" />
        </CustomButton>
      </View>
    </>
  )
}

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    flexDirection: 'row',
    width: '100%',
    height: '6%',
    backgroundColor: 'black',
  },
})

export default Navbar