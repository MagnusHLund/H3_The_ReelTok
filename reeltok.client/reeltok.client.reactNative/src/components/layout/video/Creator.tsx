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
  
  const isCreator = user.userId === video.creatorUserId

  return (
    <View>
      {isCreator ? (
        <View style={styles.container}>
          <View style={styles.pictureContainer}>
            <CreatorImage profilePictureUrl={user.profilePictureUrl} />
          </View>
          <View style={styles.textContainer}>
            <Username username={user.username} />
            <Title title={video.title} />
            <Description description={video.description} />
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
    width: 400,
    left: -30,
    top: '60%',
  },
  pictureContainer: {
    left: -50,
    width: '20%',
    justifyContent: 'flex-start',
    top: -100,
  },
  textContainer: {
    flexDirection: 'column',
    flex: 1,
  },
})

export default Creator
