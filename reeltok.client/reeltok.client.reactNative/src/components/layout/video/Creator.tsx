import { View, StyleSheet } from 'react-native'
import React from 'react'
import Username from './Username'
import Title from './Title'
import Description from './Description'
import CreatorImage from './CreatorImage'
import { UserDetails } from '../../../redux/slices/usersSlice'
import { Video } from '../../../redux/slices/videosSlice'

interface CreatorProps {
  user: UserDetails
  video: Video
}

const Creator: React.FC<CreatorProps> = ({ user, video }) => {
  const username = user.username
  const profilePictureUrl = user.profilePictureUrl
  const videoTitle = video.title
  const videoDescription = video.description

  const isCreator = user.userId === video.creatorUserId

  return (
    <View>
      {isCreator ? (
        <View style={styles.container}>
          <View style={styles.pictureContainer}>
            <CreatorImage profilePictureUrl={profilePictureUrl} />
          </View>
          <View style={styles.textContainer}>
            <Username username={username} />
            <Title title={videoTitle} />
            <Description description={videoDescription} />
          </View>
        </View>
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
    width: 300,
  },
  pictureContainer: {
    left: -50,
    width: '20%',
    justifyContent: 'flex-start',
  },
  textContainer: {
    flexDirection: 'column',
    flex: 1,
  },
})

export default Creator
