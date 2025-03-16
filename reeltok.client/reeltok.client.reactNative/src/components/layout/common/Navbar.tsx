import useAppBackHandler from '../../../hooks/useAppBackHandler'
import useAppNavigation from '../../../hooks/useAppNavigation'
import useAppDimensions from '../../../hooks/useAppDimensions'
import CustomButton from '../../input/CustomButton'
import { View, StyleSheet } from 'react-native'
import MediaSelector from './MediaSelector'
import RotatingIcon from './RotatingIcon'
import React, { useEffect, useState } from 'react'
import useAppSelector from '../../../hooks/useAppSelector'

const Navbar: React.FC = () => {
  const navigateToScreen = useAppNavigation()
  const [displayMediaSelector, setDisplayMediaSelector] = useState(false)

  const { navbarHeight } = useAppDimensions()

  const user = useAppSelector((state) => state.users.myUser)
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  useAppBackHandler()

  useEffect(() => {
    if (user?.userId) {
      setIsLoggedIn(true)
    } else {
      setIsLoggedIn(false)
    }
  }, [user])

  const toggleMediaSelector = () => {
    setDisplayMediaSelector(!displayMediaSelector)
  }

  return (
    <>
      <View style={{ display: displayMediaSelector ? 'flex' : 'none' }}>
        <MediaSelector handleSelectMedia={toggleMediaSelector} />
      </View>
      <View style={[styles.container, { height: navbarHeight }]}>
        <CustomButton transparent={true} onPress={() => navigateToScreen('VideoFeed')}>
          <RotatingIcon name="play" color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={toggleMediaSelector}>
          <RotatingIcon name="add" color="white" />
        </CustomButton>
        <CustomButton
          transparent={true}
          onPress={() => navigateToScreen(isLoggedIn ? 'Profile' : 'Login')}
        >
          <RotatingIcon name="person-circle-sharp" color="white" />
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
    backgroundColor: 'black',
  },
})

export default Navbar
