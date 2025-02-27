import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { useNavigation } from '@react-navigation/native'
import { ScreenName } from '../Navigation/Router'

const useAppNavigation = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()

  const navigateToScreen = (screenName: ScreenName) => {
    navigation.navigate(screenName)
  }

  return navigateToScreen
}

export default useAppNavigation
