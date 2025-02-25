import { View, StyleSheet, TouchableWithoutFeedback, Touchable } from 'react-native'
import useAppDimensions from '../../../hooks/useAppDimensions'
import { Video } from '../../../redux/slices/videosSlice'
import { useIsFocused } from '@react-navigation/native'
import { useVideoPlayer, VideoView } from 'expo-video'
import CustomButton from '../../input/CustomButton'
import { useEffect, useState } from 'react'
import User from './User'

interface VideoPlayerProps {
  videoDetails?: Video
  loopAmount?: number
  onNextVideo: () => void
}

const VideoPlayer: React.FC<VideoPlayerProps> = ({ videoDetails, loopAmount = 2, onNextVideo }) => {
  const [playCount, setPlayCount] = useState(0)
  const { contentHeight } = useAppDimensions()
  const isVideoFocused = useIsFocused()

  const player = useVideoPlayer(videoDetails?.streamUrl ?? '', (player) => {
    player.loop = true
    player.play()
  })

  useEffect(() => {
    if (isVideoFocused) {
      player.play()
    } else {
      player.pause()
    }
  }, [isVideoFocused, player])

  useEffect(() => {
    if (playCount >= loopAmount) {
      onNextVideo()
    }
  }, [playCount, loopAmount, onNextVideo])

  player.addListener('playToEnd', () => {
    setPlayCount((prevCount) => prevCount + 1)
  })

  const handlePress = () => {
    console.log(player.playing)
    if (player.playing) {
      player.pause()
    } else {
      player.play()
    }
  }

  return (
    <View style={[styles.container, { height: contentHeight }]}>
      <CustomButton onPress={() => {}} />
      <CustomButton onPress={() => {}} />
      <TouchableWithoutFeedback onPress={handlePress}>
        <View>
          <VideoView
            player={player}
            style={styles.video}
            nativeControls={false}
            contentFit="contain"
          />
        </View>
      </TouchableWithoutFeedback>
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
  video: {
    width: '100%',
    height: '100%',
    justifyContent: 'center',
  },
})

export default VideoPlayer
