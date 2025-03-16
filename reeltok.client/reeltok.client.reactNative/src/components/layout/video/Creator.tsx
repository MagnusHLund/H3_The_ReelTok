import { View, StyleSheet, TouchableOpacity } from 'react-native'
import React from 'react'
import Username from './Username'
import Title from './Title'
import Description from './Description'
import CreatorImage from './CreatorImage'
import { UserDetails } from '../../../redux/slices/usersSlice'
import { Video } from '../../../redux/slices/videosSlice'
import useAppDimensions from './../../../hooks/useAppDimensions'
import useAppNavigation from '../../../hooks/useAppNavigation'

interface CreatorProps {
  user: UserDetails
  video: Video
}

const Creator: React.FC<CreatorProps> = ({ user, video }) => {
  const isCreator = user.userId === video.creatorUserId
  const { fullWidth } = useAppDimensions()
  const navigateToScreen = useAppNavigation()

  function handleNavigation() {
    navigateToScreen('Profile', { userDetails: user })
  }

  return (
    <View>
      {isCreator ? (
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
      ) : (
        <></>
      )}
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
