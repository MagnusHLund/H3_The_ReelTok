import { View, StyleSheet, TouchableOpacity } from 'react-native'
import useAppDimensions from '../../../hooks/useAppDimensions'
import { Video } from '../../../redux/slices/videosSlice'
import { useIsFocused } from '@react-navigation/native'
import { useVideoPlayer, VideoView } from 'expo-video'
import { useEffect, useState } from 'react'
import VideoOverlay from './VideoOverlay'
import User from './User'

interface VideoPlayerProps {
  videoDetails?: Video
  loopAmount?: number
  isDisplayed: boolean
  onNextVideo: () => void
}

// TODO: Make it automatically start the video, on web
const VideoPlayer: React.FC<VideoPlayerProps> = ({
  videoDetails,
  loopAmount = 2,
  isDisplayed,
  onNextVideo,
}) => {
  const [playCount, setPlayCount] = useState(0)
  const { contentHeight } = useAppDimensions()
  const isVideoFocused = useIsFocused()

  const player = useVideoPlayer(videoDetails?.streamUrl ?? '', (player) => {
    player.loop = true
    player.play()
  })

  useEffect(() => {
    if (isDisplayed && isVideoFocused) {
      player.play()
    } else {
      player.currentTime = 0
      player.pause()
    }
  }, [isVideoFocused, player, isDisplayed])

  useEffect(() => {
    if (playCount >= loopAmount) {
      onNextVideo()
      setPlayCount(0)
    }
  }, [playCount, loopAmount, onNextVideo])

  useEffect(() => {
    const handlePlayToEnd = () => {
      setPlayCount((prevCount) => prevCount + 1)
    }

    player.addListener('playToEnd', handlePlayToEnd)

    return () => {
      player.removeListener('playToEnd', handlePlayToEnd)
    }
  }, [player])

  const handlePress = () => {
    if (player.playing) {
      player.pause()
    } else {
      player.play()
    }
  }

  if (videoDetails === undefined) {
    return <></> // TODO: Use VideoSpinner
  }

  return (
    <View style={[styles.container, { height: contentHeight }]}>
      <VideoOverlay videoDetails={videoDetails} />
      <TouchableOpacity onPress={handlePress} style={styles.touchableArea} activeOpacity={1}>
        <View style={styles.videoContainer}>
          <VideoView
            player={player}
            style={styles.video}
            nativeControls={false}
            contentFit="contain"
          />
        </View>
      </TouchableOpacity>
      <User />
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
    height: '100%',
  },
  touchableArea: {
    width: '100%',
    height: '100%',
  },
  videoContainer: {
    width: '100%',
    height: '100%',
    pointerEvents: 'none',
  },
  video: {
    width: '100%',
    height: '100%',
  },
})

export default VideoPlayer
