
import React, { useState, useRef } from 'react';
import { StyleSheet, View, FlatList, Dimensions } from 'react-native';
import Video from '../Layout/video/Video';
import Navbar from '../Layout/common/Navbar';

const { height } = Dimensions.get('window');

const VideoFeedScreen = () => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const viewabilityConfig = useRef({ viewAreaCoveragePercentThreshold: 50 }).current;

  const videoSources = [
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334929869439635536/89a449c5-62b7-487f-9874-dd580182e776.mp4?ex=67b01e12&is=67aecc92&hm=5e574b7d14fdc340ca6254c953c41b5088aa4b37790abd46c2abdd5bb78314c8&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334842524476379229/e44a31aa-05bd-4b79-86bb-51f0d1a7dd00.mp4?ex=67afccba&is=67ae7b3a&hm=473119b24ec702eacc8973fb51bd524b157dae25f265c115b2772b1dfca99cef&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334633489777295413/wVO6Mv6.mp4?ex=67af0a0c&is=67adb88c&hm=2074ad1832bd3bdcb830ff16c8c1aaf48edf7b56fa19fe6e5e1b1f31f09f67ed&",
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
        renderItem={({ item, index }) => <Video source={item} isFocused={index === currentIndex} />}
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
      <Navbar/>
    </>
  );
};

export default VideoFeedScreen;

