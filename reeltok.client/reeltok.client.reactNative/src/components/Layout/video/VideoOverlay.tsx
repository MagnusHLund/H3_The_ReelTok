import { useAppDispatch } from '../../../hooks/useAppDispatch'
import { Video } from '../../../redux/slices/videosSlice'
import { View, Text, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import { Ionicons } from '@expo/vector-icons'
import React, { useState } from 'react'
import { hasLikedVideoThunk } from '../../../redux/thunks/videosThunks'
import CommentSection from './CommentsSection'

interface VideoOverlayProps {
  videoDetails: Video
}

// TODO: Use icons & text font with an outline, so they are visible on any video background!
const VideoOverlay: React.FC<VideoOverlayProps> = ({ videoDetails }) => {
  const dispatch = useAppDispatch()
  const [showCommentsSection, setShowCommentsSection] = useState(false)

  const handleLikeButtonPress = () => {
    console.log('test')
    dispatch(hasLikedVideoThunk(videoDetails))
  }
  const handleCommentsButtonPress = () => {}

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
          onPress={handleCommentsButtonPress}
        >
          <Ionicons name="chatbubble-outline" size={32} color="white" />
        </CustomButton>
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
})

export default VideoOverlay
