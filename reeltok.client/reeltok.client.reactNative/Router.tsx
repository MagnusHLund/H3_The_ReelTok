import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

import VideoFeedScreen from './components/screens/VideoFeedScreen';
import UploadVideoScreen from './components/screens/UploadVideoScreen';
import UserProfileScreen from './components/screens/UserProfileScreen';

const Stack = createNativeStackNavigator();

const Router = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen name="VideoFeed" component={VideoFeedScreen} options={{ headerShown: false }} />
        <Stack.Screen name="UploadVideo" component={UploadVideoScreen} />
        <Stack.Screen name="Profile" component={UserProfileScreen} options={{ headerShown: false }}/>
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default Router;


