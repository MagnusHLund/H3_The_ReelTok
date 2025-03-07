import { changeLanguageThunk } from '../redux/thunks/settingsThunks'
import { createStackNavigator } from '@react-navigation/stack'
import * as ScreenOrientation from 'expo-screen-orientation'
import { NavigationContainer } from '@react-navigation/native'
import stackScreens, { screensWebConfig } from './Routes'
import Navbar from '../components/LayoutTemp/common/Navbar'
import useAppDispatch from '../hooks/useAppDispatch'
import useAppSelector from '../hooks/useAppSelector'
import useGeoLocation from '../hooks/useGeoLocation'
import { View, StyleSheet } from 'react-native'
import React, { useEffect } from 'react'

export type ScreenName =
  | 'VideoFeed'
  | 'UploadVideo'
  | 'Profile'
  | 'Settings'
  | 'Subscriptions'
  | 'Subscribers'
  | 'Login'
  | 'Signup'
  | 'Camera'

const Stack = createStackNavigator()

const linking = {
  prefixes: ['http://localhost:8081'],
  config: {
    screens: screensWebConfig,
  },
}

const Router: React.FC = () => {
  const dispatch = useAppDispatch()
  const { country } = useGeoLocation()
  const language = useAppSelector((state) => state.settings.language)

  useEffect(() => {
    if (!language || !language.locale) {
      const selectedLanguage = country === 'Denmark' ? 'da_DK' : 'en_GB'
      dispatch(
        changeLanguageThunk({
          label: selectedLanguage === 'da_DK' ? 'Danish' : 'English',
          value: selectedLanguage,
        })
      )
    }
  }, [country, language, dispatch])

  useEffect(() => {
    const lockOrientation = async () => {
      await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.PORTRAIT_UP)
    }
    lockOrientation()
  }, [])

  return (
    <NavigationContainer linking={linking}>
      <View style={styles.rootContainer}>
        <Stack.Navigator initialRouteName="VideoFeed" detachInactiveScreens>
          {stackScreens.map((screen) => (
            <Stack.Screen
              key={screen.name}
              name={screen.name}
              component={screen.component}
              options={{
                headerShown: false,
                cardStyleInterpolator: screen.navigationAnimation,
              }}
            />
          ))}
        </Stack.Navigator>
        <Navbar />
      </View>
    </NavigationContainer>
  )
}

const styles = StyleSheet.create({
  rootContainer: {
    flexDirection: 'column',
    height: '100%',
    width: '100%',
  },
})

export default Router
