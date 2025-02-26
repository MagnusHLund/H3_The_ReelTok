import React, { useState } from 'react'
import { View, StyleSheet } from 'react-native'
import { useNavigation } from '@react-navigation/native'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import CustomButton from '../../input/CustomButton'
import MediaSelector from './MediaSelector'
import RotatingIcon from './RotatingIcon'

const Navbar: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()
  const [displayMediaSelector, setDisplayMediaSelector] = useState(false)

  const toggleMediaSelectorForVideoUpload = () => {
    setDisplayMediaSelector(!displayMediaSelector)
  }

  return (
    <>
      {displayMediaSelector && <MediaSelector />}
      <View style={styles.container}>
        <CustomButton transparent={true} onPress={() => navigation.replace('VideoFeed')}>
          <RotatingIcon name="play" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={toggleMediaSelectorForVideoUpload}>
          <RotatingIcon name="add" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={() => navigation.replace('Profile')}>
          <RotatingIcon name="person-circle-sharp" size={32} color="white" />
        </CustomButton>
      </View>
    </>
  )
}

const styles = StyleSheet.create({
  container: {
    display: 'flex',
    position: 'absolute',
    bottom: 0,
    justifyContent: 'space-evenly',
    alignItems: 'center',
    flexDirection: 'row',
    width: '100%',
    height: '6%',
    backgroundColor: 'black',
    zIndex: 110,
  },
})

export default Navbar
