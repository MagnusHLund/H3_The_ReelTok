import { View, Modal, ActivityIndicator, StyleSheet } from 'react-native'
import React from 'react'

interface VideoSpinnerProps {
  awaitingVideo: boolean
}

const VideoSpinner: React.FC<VideoSpinnerProps> = ({ awaitingVideo }) => {
  return (
    <Modal visible={awaitingVideo} transparent={true}>
      <View style={styles.container}>
        <ActivityIndicator size="large" color="#ff0050" />
      </View>
    </Modal>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
  },
})

export default VideoSpinner
