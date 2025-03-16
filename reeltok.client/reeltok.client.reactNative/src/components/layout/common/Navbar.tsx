import useAppBackHandler from '../../../hooks/useAppBackHandler'
import useAppNavigation from '../../../hooks/useAppNavigation'
import useAppDimensions from '../../../hooks/useAppDimensions'
import CustomButton from '../../input/CustomButton'
import { View, StyleSheet, Modal, TouchableWithoutFeedback, Keyboard } from 'react-native'
import MediaSelector from './MediaSelector'
import RotatingIcon from './RotatingIcon'
import React, { useEffect, useState } from 'react'
import { UserDetails } from '../../../redux/slices/usersSlice'
import useAppSelector from '../../../hooks/useAppSelector'

const Navbar: React.FC = () => {
  const [displayMediaSelector, setDisplayMediaSelector] = useState(false)
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  const [keyboardVisible, setKeyboardVisible] = useState(false)
  const { navbarHeight } = useAppDimensions()
  const user = useAppSelector((state) => state.users.myUser)
  const navigateToScreen = useAppNavigation()
  useAppBackHandler()

  useEffect(() => {
    if (user?.userId) {
      setIsLoggedIn(true)
    } else {
      setIsLoggedIn(false)
    }

    const showSubscription = Keyboard.addListener('keyboardDidShow', () => setKeyboardVisible(true))
    const hideSubscription = Keyboard.addListener('keyboardDidHide', () =>
      setKeyboardVisible(false)
    )

    return () => {
      showSubscription.remove()
      hideSubscription.remove()
    }
  }, [user])

  const toggleMediaSelector = () => {
    setDisplayMediaSelector(!displayMediaSelector)
  }

  const defaultUser: UserDetails = {
    userId: 'guidUserId3',
    username: 'Magnus',
    email: 'someUrl.com',
    profilePictureUrl: 'https://avatars.githubusercontent.com/u/124877369?v=4',
  }

  return (
    <>
      <Modal
        visible={displayMediaSelector}
        transparent={true}
        onRequestClose={toggleMediaSelector}
        animationType="none"
      >
        <TouchableWithoutFeedback onPress={toggleMediaSelector}>
          <View style={{ flex: 1 }}>
            <MediaSelector handleSelectMedia={toggleMediaSelector} />
          </View>
        </TouchableWithoutFeedback>
      </Modal>
      {!keyboardVisible && (
        <View style={[styles.container, { height: navbarHeight }]}>
          <CustomButton transparent={true} onPress={() => navigateToScreen('VideoFeed')}>
            <RotatingIcon name="play" color="white" />
          </CustomButton>
          <CustomButton transparent={true} onPress={toggleMediaSelector}>
            <RotatingIcon name="add" color="white" />
          </CustomButton>
          <CustomButton
            transparent={true}
            onPress={() =>
              navigateToScreen(
                isLoggedIn ? 'Profile' : 'Login',
                isLoggedIn ? { userDetails: defaultUser } : undefined
              )
            }
          >
            <RotatingIcon name="person-circle-sharp" color="white" />
          </CustomButton>
        </View>
      )}
    </>
  )
}

const styles = StyleSheet.create({
  container: {
    position: 'absolute',
    bottom: 0,
    left: 0,
    right: 0,
    alignItems: 'center',
    flexDirection: 'row',
    width: '100%',
    backgroundColor: 'black',
  },
})

export default Navbar
