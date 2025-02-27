import { createStackNavigator, StackCardStyleInterpolator } from '@react-navigation/stack'
import { changeLanguageThunk } from '../redux/thunks/settingsThunks'
import { NavigationContainer } from '@react-navigation/native'
import stackScreens, { screensWebConfig } from './Routes'
import Navbar from '../components/Layout/common/Navbar'
import useAppDispatch from '../hooks/useAppDispatch'
import useAppSelector from '../hooks/useAppSelector'
import useGeoLocation from '../hooks/useGeoLocation'
import React, { useEffect } from 'react'

export type ScreenName =
  | 'VideoFeed'
  | 'UploadVideo'
  | 'Profile'
  | 'Settings'
  | 'Subscriptions'
  | 'Subscribers'

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

  return (
    <NavigationContainer linking={linking}>
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
    </NavigationContainer>
  )
}

export default Router
