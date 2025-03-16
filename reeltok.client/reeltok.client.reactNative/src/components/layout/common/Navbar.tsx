import useAppBackHandler from '../../../hooks/useAppBackHandler'
import useAppNavigation from '../../../hooks/useAppNavigation'
import useAppDimensions from '../../../hooks/useAppDimensions'
import CustomButton from '../../input/CustomButton'
import { View, StyleSheet, Modal, TouchableWithoutFeedback } from 'react-native'
import MediaSelector from './MediaSelector'
import RotatingIcon from './RotatingIcon'
import React, { useState } from 'react'
import { UserDetails } from '../../../redux/slices/usersSlice'

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

  const defaultUser: UserDetails = {
    userId: 'guidUserId3',
    username: 'Magnus',
    profileUrl: 'someurl.com',
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
      <View style={[styles.container, { height: navbarHeight }]}>
        <CustomButton transparent={true} onPress={() => navigateToScreen('VideoFeed')}>
          <RotatingIcon name="play" color="white" />
        </CustomButton>
        <CustomButton transparent={true} onPress={toggleMediaSelector}>
          <RotatingIcon name="add" color="white" />
        </CustomButton>
        <CustomButton
          transparent={true}
          onPress={() => navigateToScreen('Profile', { userDetails: defaultUser })}
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
