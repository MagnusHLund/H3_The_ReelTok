import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { useNavigation } from '@react-navigation/native'
import { ScreenName } from '../navigation/Router'

const useAppNavigation = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()

  const navigateToScreen = (screenName: ScreenName, params?: unknown) => {
    navigation.navigate(screenName, params)
  }

  return navigateToScreen
}

export default useAppNavigation
