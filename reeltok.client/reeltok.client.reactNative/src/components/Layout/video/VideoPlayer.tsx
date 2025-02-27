import { View, StyleSheet, TouchableOpacity } from 'react-native'
import React from 'react'
import { useVideoPlayer, VideoView } from 'expo-video'

interface VideoPlayerProps {
  uri: string
}
const VideoPlayer: React.FC<VideoPlayerProps> = ({ uri }) => {
  const player = useVideoPlayer(uri ?? '', (player) => {
    player.loop = true
    player.play()
  })
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
