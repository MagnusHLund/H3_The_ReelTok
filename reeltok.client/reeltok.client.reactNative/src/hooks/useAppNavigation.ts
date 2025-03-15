import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { useNavigation } from '@react-navigation/native'
import { ScreenName } from '../navigation/Router'
import { UserDetails } from '../redux/slices/usersSlice'

const useAppNavigation = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()

  const navigateToScreen = (screenName: ScreenName, params?: { userDetails?: UserDetails }) => {
    navigation.navigate(screenName, params)
  }

  return navigateToScreen
}

export default useAppNavigation
