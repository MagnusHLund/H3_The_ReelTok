import { CardStyleInterpolators, StackCardStyleInterpolator } from '@react-navigation/stack'
import { ScreenName } from './Router'
import VideoFeedScreen from '../components/screens/VideoFeedScreen'
import UploadVideoScreen from '../components/screens/UploadVideoScreen'
import UserProfileScreen from '../components/screens/UserProfileScreen'
import SettingsScreen from '../components/screens/SettingsScreen'
import SubscriptionsScreen from '../components/screens/SubscriptionsScreen'
import SubscribersScreen from '../components/screens/SubscribersScreen'

interface StackScreenProps {
  name: ScreenName
  webRoute: string
  component: React.FC
  navigationAnimation?: StackCardStyleInterpolator
}

const stackScreens: StackScreenProps[] = [
  {
    name: 'VideoFeed',
    webRoute: '',
    component: VideoFeedScreen,
    navigationAnimation: CardStyleInterpolators.forHorizontalIOSInverted,
  },
  {
    name: 'UploadVideo',
    webRoute: 'upload',
    component: UploadVideoScreen,
  },
  {
    name: 'Profile',
    webRoute: 'profile',
    component: UserProfileScreen,
    navigationAnimation: CardStyleInterpolators.forHorizontalIOS,
  },
  {
    name: 'Settings',
    webRoute: 'settings',
    component: SettingsScreen,
    navigationAnimation: CardStyleInterpolators.forHorizontalIOS,
  },
  {
    name: 'Subscriptions',
    webRoute: 'subscriptions',
    component: SubscriptionsScreen,
    navigationAnimation: CardStyleInterpolators.forHorizontalIOS,
  },
  {
    name: 'Subscribers',
    webRoute: 'subscribers',
    component: SubscribersScreen,
    navigationAnimation: CardStyleInterpolators.forHorizontalIOS,
  },
]

export const screensWebConfig = stackScreens.reduce(
  (acc: { [key: string]: any }, stackScreen: StackScreenProps) => {
    acc[stackScreen.name] = { path: stackScreen.webRoute }
    return acc
  },
  {}
)

export default stackScreens
