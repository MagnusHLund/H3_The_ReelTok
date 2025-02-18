import { useVideoPlayer, VideoView } from 'expo-video';
import { useEvent } from 'expo';
import { StyleSheet, View, Dimensions, TouchableWithoutFeedback } from 'react-native';
import CustomButton from '../../input/CustomButton';
import Ionicons from '@expo/vector-icons/Ionicons';
import { useState } from 'react';

interface VideoProps {
  source: string,
  isFocused: boolean,
}

const { width, height } = Dimensions.get("window");

const Video: React.FC<VideoProps> = ({ source, isFocused }) => {
  const player = useVideoPlayer(source, player => {
    player.loop = true;
  });

  const isPlaying = useEvent(player, 'playingChange');

  if (isFocused) {
    player.play();
  } else {
    player.pause();
  }

  const togglePlayPause = () => {
    if (isPlaying) {
      player.pause();
    } else {
      player.play();
    }
  };

  const [likedVideo, setLikedVideo] = useState(false);

  return (
    <>
      <TouchableWithoutFeedback style={{ zIndex: 120 }} onPress={() => togglePlayPause}>
      <View style={styles.contentContainer}>
        <VideoView style={styles.video} player={player} contentFit="contain" nativeControls={false} />
        <View style={styles.playButton}>
          {isPlaying ? (
            null
          ) : (
            <Ionicons name="play" size={64} color="white" />
          )}
        </View>
      </View>
      </TouchableWithoutFeedback>
      
      <View style={styles.socialControls}>
        <CustomButton transparent={true} borders={false} onPress={() => setLikedVideo(!likedVideo)}>
          <Ionicons name={likedVideo ? 'heart' : 'heart-outline'} size={32} color={likedVideo ? 'red' : 'white'} />
        </CustomButton>
      </View>
    </>
  );
};

const styles = StyleSheet.create({
  contentContainer: {
    width: width,
    height: height,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 50,
    margin: 0,
  },
  video: {
    width: width,
    height: height,
  },
  playButton: {
    position: 'absolute',
    height: '100%',
    width: '100%',
    top: '50%',
    bottom: '50%',
    left: '55%',
    right: '50%',
    zIndex: 110,
  },
  socialControls: {
    position: 'absolute',
    top: '50%',
    bottom: '50%',
    left: '78%',
    right: 0,
    margin: 0,
    display: 'flex',
    flexDirection: 'column',
    height: '100%',
    width: '12.5%',
    zIndex: 110,
  },
});

export default Video;
