import { useVideoPlayer, VideoView } from 'expo-video';
import { Animated, StyleSheet, View, Dimensions, Pressable, Text } from 'react-native';
import { useIsFocused } from '@react-navigation/native';
import CustomButton from '../../input/CustomButton';
import ProfilePicture from '../profile/ProfilePicture';
import ExpandableView from '../common/ExpandableView';
import Ionicons from '@expo/vector-icons/Ionicons';
import { useState, useEffect } from 'react';

interface VideoProps {
  source: string,
  creator: { profilePictureUrl: string; username: string },
  description?: string,
  isFocused: boolean,
  onShowComments: () => void;
}

const { width, height } = Dimensions.get("window");

const Video: React.FC<VideoProps> = ({ source, creator, description, isFocused, onShowComments }) => {

  const player = useVideoPlayer(source, player => {
    player.loop = true;
  });

  const [isPlaying, setIsPlaying] = useState(false);
  const isScreenFocused = useIsFocused();

  useEffect(() => {
    if (isFocused && isScreenFocused) {
      player.play();
      setIsPlaying(true);
    } else {
      player.pause();
      setIsPlaying(false);
    }
  }, [isFocused, isScreenFocused]); 

  const togglePlayPause = () => {
    if (isPlaying) {
      player.pause();
      setIsPlaying(false);
    } else {
      player.play();
      setIsPlaying(true);
    }
  };

  const [likedVideo, setLikedVideo] = useState(false);
  const [followedCreator, setFollowedCreator] = useState(false);
  const [isExpanded, setIsExpanded] = useState(false);
    const profileOverlayBottom = new Animated.Value(130);
  const likesAmount = 50;
  const commentsAmount = 10;

  useEffect(() => {
    Animated.timing(profileOverlayBottom, {
      toValue: isExpanded ? 280 : 130, // Move up when expanding
      duration: 150,
      useNativeDriver: false,
    }).start();
  }, [isExpanded]);

  return (
    <>
      <View style={styles.contentContainer} onStartShouldSetResponder={() => true} onResponderRelease={togglePlayPause}>
        <VideoView style={styles.video} player={player} contentFit="contain" nativeControls={false} />
        <Pressable style={StyleSheet.absoluteFill} onPress={togglePlayPause} />
      </View>
      <Animated.View style={[styles.profileOverlay, { bottom: profileOverlayBottom }]}>
        <ProfilePicture pictureUrl={creator.profilePictureUrl} />
        <Text style={styles.socialFontSettings}>{creator.username}</Text>
        <CustomButton
          transparent={false}
          widthPercentage={0.25}
          title={followedCreator ? 'Following' : 'Follow'}
          onPress={() => setFollowedCreator(!followedCreator)}
        />
        <Text style={{ color: 'black' }} onPress={() => setIsExpanded(!isExpanded)}>
          Expand
        </Text>
      </Animated.View>
      <ExpandableView expanded={isExpanded}>
        <Text> {description} </Text>
      </ExpandableView>      <View style={styles.socialControls}>
        <CustomButton transparent={true} borders={false} flexDirection='column' onPress={() => setLikedVideo(!likedVideo)}>
          <Ionicons name={likedVideo ? 'heart' : 'heart-outline'} size={32} color={likedVideo ? 'red' : 'white'} />
          <Text style={styles.socialFontSettings}> {likesAmount} </Text>
        </CustomButton>
        <CustomButton transparent={true} borders={false} flexDirection='column' onPress={onShowComments}>
          <Ionicons name="chatbubble-outline" size={32} color="white" />
          <Text style={styles.socialFontSettings}> {commentsAmount} </Text>
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
    zIndex: 110,
  },
  video: {
    width: width,
    height: height,
  },
  profileOverlay: {
    position: 'absolute',
    bottom: 130,
    left: '27.5%',
    width: 75,
    height: 30,
    alignItems: 'center',
    justifyContent: 'center',
    flexDirection: 'row',
    gap: 15,
    zIndex: 120,
  },
  videoDetails: {
    display: 'flex',
    flexDirection: 'column',
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
  socialFontSettings: {
    color: 'white',
    fontSize: 15,
  }
});

export default Video;
