import { createStackNavigator, CardStyleInterpolators } from '@react-navigation/stack'
import UploadVideoScreen from './components/screens/UploadVideoScreen'
import UserProfileScreen from './components/screens/UserProfileScreen'
import VideoFeedScreen from './components/screens/VideoFeedScreen'
import SettingsScreen from './components/screens/SettingsScreen'
import { NavigationContainer } from '@react-navigation/native'
import React from 'react'

export type ScreenName = 'VideoFeed' | 'UploadVideo' | 'Profile' | 'Settings'

const Stack = createStackNavigator()

const Router = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="VideoFeed" detachInactiveScreens>
        <Stack.Screen
          name="VideoFeed"
          component={VideoFeedScreen}
          options={{
            headerShown: false,
            cardStyleInterpolator: CardStyleInterpolators.forHorizontalIOSInverted,
          }}
        />
        <Stack.Screen
          name="UploadVideo"
          component={UploadVideoScreen}
          options={{
            headerShown: false,
          }}
        />
        <Stack.Screen
          name="Profile"
          component={UserProfileScreen}
          options={{
            headerShown: false,
            cardStyleInterpolator: CardStyleInterpolators.forHorizontalIOS,
          }}
        />
        <Stack.Screen
          name="Settings"
          component={SettingsScreen}
          options={{
            headerShown: false,
            cardStyleInterpolator: CardStyleInterpolators.forHorizontalIOS,
          }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  )
}

export default Router
