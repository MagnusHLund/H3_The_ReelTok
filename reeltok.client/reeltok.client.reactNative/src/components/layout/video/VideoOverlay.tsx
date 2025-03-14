import { hasLikedVideoThunk } from '../../../redux/thunks/videosThunks'
import useAppDispatch from '../../../hooks/useAppDispatch'
import { Video } from '../../../redux/slices/videosSlice'
import { View, Text, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import { Ionicons } from '@expo/vector-icons'
import useOrientation from '../../../hooks/useOrientation'
import React from 'react'
import ProfileImage from '../profile/ProfileImage'
import useAppDimensions from '../../../hooks/useAppDimensions'
import useAppSelector from '../../../hooks/useAppSelector'
import Creator from './Creator'
import { UserDetails } from '../../../redux/slices/usersSlice'

interface VideoOverlayProps {
  videoDetails: Video
  userdetails: UserDetails
  onCommentsOpen: () => void
}

// TODO: Use icons & text font with an outline, so they are visible on any video background!
// TODO: Add video creator and video information to the overlay
const VideoOverlay: React.FC<VideoOverlayProps> = ({
  videoDetails,
  userdetails,
  onCommentsOpen,
}) => {
  const dispatch = useAppDispatch()
  const orientation = useOrientation('', 'VideoOverlay')
  const { fullWidth, contentHeight } = useAppDimensions()

  const user = useAppSelector((state) => state.users.users.find((user) => user))
  const video = useAppSelector((state) => state.videos.videos.find((video) => video))

  const handleLikeButtonPress = () => {
    dispatch(hasLikedVideoThunk(videoDetails))
  }

  return (
    <View style={[styles.container, { width: fullWidth, height: contentHeight }]}>
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
        <Creator user={userdetails} video={videoDetails} />
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    position: 'absolute',
    justifyContent: 'center',
    right: 1,
  },
  icons: {
    position: 'absolute',
    right: '-1%',
    zIndex: 1,
  },
  iconText: {
    color: 'white',
  },
  aboutContainer: {
    position: 'absolute',

    bottom: '10%',
  },
})

export default VideoOverlay
