import useAppBackHandler from '../../../hooks/useAppBackHandler'
import useAppNavigation from '../../../hooks/useAppNavigation'
import useAppDimensions from '../../../hooks/useAppDimensions'
import CustomButton from '../../input/CustomButton'
import { View, StyleSheet } from 'react-native'
import MediaSelector from './MediaSelector'
import RotatingIcon from './RotatingIcon'
import React, { useState } from 'react'

// TODO: Cant navigate to same page as you are already on.
const Navbar: React.FC = () => {
  const navigateToScreen = useAppNavigation()
  const [displayMediaSelector, setDisplayMediaSelector] = useState(false)
  const { navbarHeight } = useAppDimensions()
  useAppBackHandler()

  const toggleMediaSelectorForVideoUpload = () => {
    setDisplayMediaSelector(!displayMediaSelector)
  }

  return (
    <>
      {displayMediaSelector && <MediaSelector />}
      <View style={[styles.container, { height: navbarHeight }]}>
        <CustomButton transparent={true} onPress={() => navigateToScreen('VideoFeed')}>
          <RotatingIcon name="play" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={toggleMediaSelectorForVideoUpload}>
          <RotatingIcon name="add" size={32} color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={() => navigateToScreen('Profile')}>
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
