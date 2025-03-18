import { View, StyleSheet, TouchableOpacity } from 'react-native'
import React from 'react'
import Username from './Username'
import Title from './Title'
import Description from './Description'
import CreatorImage from './CreatorImage'
import { Video } from '../../../redux/slices/videosSlice'
import useAppDimensions from './../../../hooks/useAppDimensions'
import useAppNavigation from '../../../hooks/useAppNavigation'
import useAppSelector from '../../../hooks/useAppSelector'

interface CreatorProps {
  userId: string
  video: Video
}

const Creator: React.FC<CreatorProps> = ({ userId, video }) => {
  const { fullWidth } = useAppDimensions()
  const navigateToScreen = useAppNavigation()

  // Fetch the user details from the global state
  const user = useAppSelector((state) => state.users.users.find((user) => user.userId === userId))
  console.log(user)
  console.log(userId)
  function handleNavigation() {
    navigateToScreen('Profile', { userDetails: userId })
  }

  if (!user) {
    return null // or a loading indicator
  }

  return (
    <View>
      <TouchableOpacity onPress={handleNavigation} style={styles.touch}>
        <View style={[styles.container, { width: fullWidth }]}>
          <View style={styles.pictureContainer}>
            <CreatorImage profilePictureUrl={user.profilePictureUrl} />
          </View>
          <View style={styles.textContainer}>
            <Username username={user.username} />
            <Title title={video.title} />
            <Description description={video.description} />
          </View>
        </View>
      </TouchableOpacity>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    position: 'absolute',
    flexDirection: 'row',
    justifyContent: 'flex-start',
    top: '0%',
  },
  pictureContainer: {
    left: '5%',
    width: '20%',
    justifyContent: 'flex-start',
  },
  textContainer: {
    flexDirection: 'column',
    flex: 1,
  },
  touch: {
    zIndex: 2,
  },
})

export default Creator
