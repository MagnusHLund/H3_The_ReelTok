import { useNavigation, CommonActions } from '@react-navigation/native'
import { BackHandler } from 'react-native'
import { useEffect } from 'react'

const useAppBackHandler = () => {
  const navigation = useNavigation()

  useEffect(() => {
    const handleBackPress = () => {
      if (navigation.canGoBack()) {
        const state = navigation.getState()
        if (state === undefined) {
          return
        }

        const routes = state.routes
        if (routes.length > 2) {
          const lastRoute = routes[routes.length - 2]
          navigation.dispatch(
            CommonActions.reset({
              index: 1,
              routes: [{ key: lastRoute.key, name: lastRoute.name }],
            })
          )
        } else {
          navigation.goBack()
        }
        return true
      } else {
        return false
      }
    }

    BackHandler.addEventListener('hardwareBackPress', handleBackPress)

    return () => {
      BackHandler.removeEventListener('hardwareBackPress', handleBackPress)
    }
  }, [navigation])
}

export default useAppBackHandler
