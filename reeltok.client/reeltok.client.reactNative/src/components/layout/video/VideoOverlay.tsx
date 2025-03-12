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

// import { Video } from 'expo' video is getting imported twice i commented this out because im unsure which is being used i just think the other one is more likely

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
  const { fullWidth } = useAppDimensions()

  const user = useAppSelector((state) => state.users.users.find((user) => user))
  const video = useAppSelector((state) => state.videos.videos.find((video) => video))

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
        <Creator user={userdetails} video={videoDetails} />
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
    zIndex: 1,
    top: '350%',
    right: '900%',
    height: '50%',
  },
})

export default VideoOverlay
