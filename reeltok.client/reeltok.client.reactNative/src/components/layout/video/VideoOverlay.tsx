import { hasLikedVideoThunk } from '../../../redux/thunks/videosThunks'
import useAppDispatch from '../../../hooks/useAppDispatch'
import { Video } from '../../../redux/slices/videosSlice'
import { View, Text, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import { Ionicons } from '@expo/vector-icons'
import useOrientation from '../../../hooks/useOrientation'
import React from 'react'
import ProfileImage from '../profile/ProfileImage'
import ProfilePicture from '../common/ProfilePicture'

interface VideoOverlayProps {
  videoDetails: Video
  onCommentsOpen: () => void
}

// TODO: Use icons & text font with an outline, so they are visible on any video background!
// TODO: Add video creator and video information to the overlay
const VideoOverlay: React.FC<VideoOverlayProps> = ({ videoDetails, onCommentsOpen }) => {
  const dispatch = useAppDispatch()
  const orientation = useOrientation('', 'VideoOverlay')
  const handleLikeButtonPress = () => {
    dispatch(hasLikedVideoThunk(videoDetails))
  }

  return (
    <View style={styles.container}>
      <View style={styles.icons}>
        <CustomButton
          transparent
          borders={false}
          flexDirection="column"
          widthPercentage={'auto'}
          onPress={handleLikeButtonPress}
        >
          <Ionicons
            name={videoDetails.hasLiked ? 'heart' : 'heart-outline'}
            size={32}
            color={videoDetails.hasLiked ? 'red' : 'white'}
          />
          <Text style={styles.iconText}>{videoDetails.likes}</Text>
        </CustomButton>
      </View>
      <View style={styles.icons}>
        <CustomButton
          transparent
          borders={false}
          flexDirection="column"
          widthPercentage={'auto'}
          onPress={onCommentsOpen}
        >
          <Ionicons name="chatbubble-outline" size={32} color="white" />
        </CustomButton>
      </View>
      <View style={styles.aboutContainer}>
        <View style={styles.profilePictureContainer}>
          <ProfileImage
            source={require('./../../../../assets/images/placeholders/profile-default-img.png')}
            width={40}
            height={40}
            allowedToChangePicture={true}
          />
        </View>
        <View style={styles.usernameContainer}>
          <Text>username</Text>
        </View>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    position: 'absolute',
    justifyContent: 'center',
    zIndex: 1,
    right: 0,
  },
  icons: {
    zIndex: 1,
    justifyContent: 'center',
  },
  iconText: {
    color: 'black',
  },
  aboutContainer: {
    position: 'absolute',
    flexDirection: 'row',
    zIndex: 100,
    top: '100%',
    right: '200%',
  },
  profilePictureContainer: {},
  usernameContainer: {
    zIndex: 100,
    color: 'green',
    fontSize: 30,
  },
})

export default VideoOverlay
