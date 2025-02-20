// The navbar needs to be able to be at the bottom of the screen, in vertical mode, but on the right side of the screen while in horizontal mode.
// The icons inside the navbar should turn to the correct axis, for the given screen orientation.

import React, { useEffect, useState } from 'react'
import { View, StyleSheet } from 'react-native'
import { useNavigation } from '@react-navigation/native'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import Ionicons from '@expo/vector-icons/Ionicons'
import CustomButton from '../../input/CustomButton'
import MediaSelector from './MediaSelector'
import useOrientation from '../../../hooks/useOrientation'

const Navbar: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()
  const [displayMediaSelector, setDisplayMediaSelector] = useState(false)
  const orientation = useOrientation()

  const toggleMediaSelectorForVideoUpload = () => {
    setDisplayMediaSelector(!displayMediaSelector)
  }
  useEffect(() => {
    console.log(orientation)
  }, [orientation])

  return (
    <>
      {displayMediaSelector && <MediaSelector />}
      <View style={styles.container}>
        <CustomButton transparent={true} onPress={() => navigation.replace('VideoFeed')}>
          <Ionicons name="play" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={() => toggleMediaSelectorForVideoUpload()}>
          <Ionicons name="add" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={() => navigation.replace('Profile')}>
          <Ionicons name="person-circle-sharp" size={32} color="white" />
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
