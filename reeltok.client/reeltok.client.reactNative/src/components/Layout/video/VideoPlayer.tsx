import { Video } from '../../../redux/slices/videosSlice'
import { useVideoPlayer, VideoView } from 'expo-video'
import CustomButton from '../../input/CustomButton'
import { View, StyleSheet, TouchableWithoutFeedback } from 'react-native'
import { useEffect, useState } from 'react'
import { useEvent } from 'expo'
import User from './User'

interface VideoPlayerProps {
  videoDetails?: Video
  loopAmount?: number
  onNextVideo: () => void
}

const VideoPlayer: React.FC<VideoPlayerProps> = ({ videoDetails, loopAmount = 2, onNextVideo }) => {
  const [playCount, setPlayCount] = useState(0)
  const player = useVideoPlayer(videoDetails?.streamUrl ?? '', (player) => {
    player.loop = true
    player.play()
  })

  useEffect(() => {
    if (playCount >= loopAmount) {
      onNextVideo()
    }
  }, [playCount, loopAmount, onNextVideo])

  const { isPlaying } = useEvent(player, 'playingChange', {
    isPlaying: player.playing,
  })

  useEffect(() => {
    setPlayCount((prevCount) => prevCount + 1)
  }, [isPlaying])

  const handlePress = () => {
    if (player.playing) {
      player.pause()
    } else {
      player.play()
    }
  }

  return (
    <View style={styles.container}>
      <TouchableWithoutFeedback onPress={handlePress}>
        <VideoView
          player={player}
          style={styles.video}
          nativeControls={false}
          contentFit="contain"
        />
      </TouchableWithoutFeedback>
      <CustomButton onPress={() => {}} />
      <CustomButton onPress={() => {}} />
      <User />
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
    height: '100%',
  },
  video: {
    flex: 1,
    width: '100%',
    height: '100%',
    justifyContent: 'center',
  },
})

export default VideoPlayer
