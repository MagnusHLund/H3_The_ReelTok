import createWebStorage from 'redux-persist/es/storage/createWebStorage'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { PersistConfig } from 'redux-persist'
import { Platform } from 'react-native'

const storage = Platform.OS === 'web' ? createWebStorage('local') : AsyncStorage

export const rootPersistConfig: PersistConfig<any> = {
  key: 'root',
  storage: storage,
  whitelist: ['settings'],
}

export const settingsPersistConfig: PersistConfig<any> = {
  key: 'settings',
  storage: storage,
}
