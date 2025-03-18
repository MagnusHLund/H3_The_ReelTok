import { hasLikedVideoThunk } from '../../../redux/thunks/videosThunks'
import useAppDispatch from '../../../hooks/useAppDispatch'
import { Video } from '../../../redux/slices/videosSlice'
import { View, Text, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import { Ionicons } from '@expo/vector-icons'
import useOrientation from '../../../hooks/useOrientation'
import React from 'react'
import useAppDimensions from '../../../hooks/useAppDimensions'
import Creator from './Creator'
import { UserDetails } from '../../../redux/slices/usersSlice'

interface VideoOverlayProps {
  videoDetails: Video
  userId: string
  onCommentsOpen: () => void
}

const VideoOverlay: React.FC<VideoOverlayProps> = ({ videoDetails, userId, onCommentsOpen }) => {
  const dispatch = useAppDispatch()
  const orientation = useOrientation('', 'VideoOverlay')
  const { fullWidth, contentHeight } = useAppDimensions()

  const handleLikeButtonPress = () => {
    dispatch(hasLikedVideoThunk(videoDetails))
    console.log(userId + ' overlay test')
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
        <Creator userId={userId} video={videoDetails} />
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
    zIndex: 1,
    bottom: '10%',
  },
})

export default VideoOverlay
