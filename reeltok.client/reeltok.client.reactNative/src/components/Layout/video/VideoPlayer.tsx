import { View, StyleSheet, TouchableOpacity } from 'react-native'
import React from 'react'
import { useVideoPlayer, VideoView } from 'expo-video'

interface VideoPlayerProps {
  videoDetails?: Video
  loopAmount?: number
  isDisplayed: boolean
  onAutoScroll: () => void
}

// TODO: Add play icon, when video is paused
const VideoPlayer: React.FC<VideoPlayerProps> = ({
  videoDetails,
  loopAmount = 2,
  isDisplayed,
  onAutoScroll,
}) => {
  const [playCount, setPlayCount] = useState(0)
  const { contentHeight } = useAppDimensions()
  const isVideoFocused = useIsFocused()
  const [showCommentsSection, setShowCommentsSection] = useState(false)
  const showCommentsSectionRef = useRef(showCommentsSection)

  const player = useVideoPlayer(videoDetails?.streamUrl ?? '', (player) => {
    player.loop = true
    player.play()
  })

  useEffect(() => {
    showCommentsSectionRef.current = showCommentsSection
  }, [showCommentsSection])

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
      onAutoScroll()
      setPlayCount(0)
    }
  }, [playCount, loopAmount, onAutoScroll])

  useEffect(() => {
    const handlePlayToEnd = () => {
      if (!showCommentsSectionRef.current) {
        setPlayCount((prevCount) => prevCount + 1)
      }
    }

    player.addListener('playToEnd', handlePlayToEnd)

    return () => {
      player.removeListener('playToEnd', handlePlayToEnd)
    }
  }, [player])

  const changeVideoPlayState = () => {
    if (player.playing) {
      player.pause()
    } else {
      player.play()
    }
  }
  return (
    <TouchableOpacity onPress={changeVideoPlayState} style={styles.touchableArea} activeOpacity={1}>
      <View style={styles.videoContainer}>
        <VideoView
          player={player}
          style={styles.video}
          nativeControls={false}
          contentFit="contain"
        />
      </View>
    </TouchableOpacity>
  )
}
const styles = StyleSheet.create({
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
