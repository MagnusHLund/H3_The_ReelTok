
import React, { useState, useRef } from 'react';
import { StyleSheet, View, FlatList, Dimensions } from 'react-native';
import Video from '../Layout/video/Video';
import Navbar from '../Layout/common/Navbar';

const { height } = Dimensions.get('window');

const VideoFeedScreen = () => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const viewabilityConfig = useRef({ viewAreaCoveragePercentThreshold: 50 }).current;

  const videoSources = [
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334633489777295413/wVO6Mv6.mp4?ex=67adb88c&is=67ac670c&hm=c45c6c333b4a57f6f97fe0c81f90a920da3a448f31e52b336447fd943c8beee2&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334633745122197586/rWaDN8l.mp4?ex=67adb8c9&is=67ac6749&hm=8ca5e07986a909413137092cc12c3e05646f498a300e72aea26799e8b7f8b567&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334929869439635536/89a449c5-62b7-487f-9874-dd580182e776.mp4?ex=67ae23d2&is=67acd252&hm=62e17296231957d80cf279ce0e4e5365a649aae39606cb781d3da39a878b56e5&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334842524476379229/e44a31aa-05bd-4b79-86bb-51f0d1a7dd00.mp4?ex=67add27a&is=67ac80fa&hm=373408907ab545564f372542991f6e7175927706d2a65429ed9e1c543e6e12bc&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334797433330466828/a0636f83-a11d-4500-a306-6bfa755b02f4.mp4?ex=67ae513b&is=67acffbb&hm=c876d7c2dcb2ddc8af50e6fede09ce9a082c4a86aeb1fda1d4519a229528e1e2&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334792440598630460/3335cbce-eddf-40e5-8bea-ac45da9a714d.mp4?ex=67ae4c95&is=67acfb15&hm=3a2e3b1f455b9cefe803b0121311a30d480e9cd98185e3eac7acc8900138165a&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334791558859587584/f2a8ab88-1297-4170-bfaf-3a23a2273c52.mp4?ex=67ae4bc2&is=67acfa42&hm=bf61bc754ead437105d4cd085e14a50e5de6ef47de82db14aa1c4b76dac640fe&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334610107853570138/O24eaZH.mp4?ex=67ae4b85&is=67acfa05&hm=94a2ecfca8e45ced81ab7f0f8ba9f0bc29641679d785b2553363576abbc5c813&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334608249051418686/6b11ef8d-44a0-4e50-b72c-7a093c3ba7ad.mp4?ex=67ae49ca&is=67acf84a&hm=780188e79ca1dc05e61bb5141938f9a428816b154f69f46f8ea8def81b9a41c0&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334601195821596755/9b4f76db-903e-4445-ba09-67d655afe883.mp4?ex=67ae4338&is=67acf1b8&hm=535f61267ecdda0b8f13c6d88abf9cba64ac33bf957d2ce5970f5b31f6c7dace&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334568360456753314/video.mp4?ex=67ae24a4&is=67acd324&hm=59e1abe69b36bcd12f579f1462e9ab3439e6a732c2ea6dffc619dc593449c7bc&",
    "https://cdn.discordapp.com/attachments/1158291807822680074/1334472970701377606/video.mp4?ex=67adcbcd&is=67ac7a4d&hm=f41fe49fb2329450248551e134c328731ad216f93e5fe7842372f05d61eaf8e2&",
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
        pagingEnabled
        decelerationRate="normal"
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

