import React from 'react'
import { NavigationContainer } from '@react-navigation/native'
import { createStackNavigator, CardStyleInterpolators } from '@react-navigation/stack'
import VideoFeedScreen from './components/screens/VideoFeedScreen'
import UploadVideoScreen from './components/screens/UploadVideoScreen'
import UserProfileScreen from './components/screens/UserProfileScreen'
import SettingsScreen from './components/screens/SettingsScreen'
import AutoLanguageSelector from './utils/AutoLanguageSelector'

const Stack = createStackNavigator()

const Router = () => {
  return (
    <NavigationContainer>
      {/* ✅ AutoLanguageSelector runs automatically when Router mounts */}
      <AutoLanguageSelector />
      <Stack.Navigator initialRouteName="VideoFeed">
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
