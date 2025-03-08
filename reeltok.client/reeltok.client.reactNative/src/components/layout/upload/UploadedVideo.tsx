import { View, StyleSheet, TouchableOpacity } from 'react-native'
import React from 'react'
import { useVideoPlayer, VideoView } from 'expo-video'
import CustomImage from '../common/CustomImage'
import { isImage } from '../../../utils/mediaUtils'

interface UploadedMediaProps {
  uri: string
}

const UploadedMedia: React.FC<UploadedMediaProps> = ({ uri }) => {
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
      <View style={styles.mediaContainer}>
        {isImage(uri) ? (
          <View style={styles.media}>
            <CustomImage
              source={{ uri }}
              resizeMode="cover"
              height={250}
              width={250}
              borderRadius={200}
            />
          </View>
        ) : (
          <VideoView
            player={player}
            style={styles.media}
            nativeControls={false}
            contentFit="contain"
          />
        )}
      </View>
    </TouchableOpacity>
  )
}

const styles = StyleSheet.create({
  touchableArea: {
    width: '100%',
    height: '100%',
  },
  mediaContainer: {
    width: '100%',
    height: '100%',
    pointerEvents: 'none',
    paddingTop: '10%',
    paddingBottom: '10%',
  },
  media: {
    width: '100%',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center',
  },
})

export default UploadedMedia
