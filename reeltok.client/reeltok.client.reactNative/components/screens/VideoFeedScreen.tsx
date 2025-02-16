
import React, { useState, useRef } from 'react';
import { StyleSheet, View, FlatList, Dimensions } from 'react-native';
import Video from '../Layout/video/Video';
import Navbar from '../Layout/common/Navbar';
import CommentSection from '../Layout/video/CommentsSection';
const { height } = Dimensions.get('window');

const VideoFeedScreen = () => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [showComments, setShowComments] = useState(false);
  const viewabilityConfig = useRef({ viewAreaCoveragePercentThreshold: 50 }).current;

  const videoSources = [
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4",
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/SubaruOutbackOnStreetAndDirt.mp4",
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
  ];

  const handleViewableItemsChanged = useRef(({ viewableItems }) => {
    if (viewableItems.length > 0) {
      setCurrentIndex(viewableItems[0].index);
    }
  }).current;

  return (
    <>
      <FlatList
        data={videoSources}
        renderItem={({ item, index }) => 
          <Video source={item} isFocused={index === currentIndex} onShowComments={() => setShowComments(true)} />
        }
        keyExtractor={(_, index) => index.toString()}
        decelerationRate="normal"
        disableIntervalMomentum={true}
        style={{ backgroundColor: 'black' }}
        showsVerticalScrollIndicator={false}
        onViewableItemsChanged={handleViewableItemsChanged}
        viewabilityConfig={viewabilityConfig}
        snapToInterval={height}
        snapToAlignment="center"
        getItemLayout={(_, index) => ({
          length: height,
          offset: height * index,
          index,
        })}
      />
      <CommentSection showComments={showComments} onClose={() => setShowComments(false)}/>
      <Navbar/>
    </>
  );
};

export default VideoFeedScreen;

