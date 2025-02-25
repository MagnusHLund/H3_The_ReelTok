import { rootPersistConfig, settingsPersistConfig } from './config/persistConfig'
import { configureStore, combineReducers, Reducer } from '@reduxjs/toolkit'
import settingsReducer, { SettingsProps } from './slices/settingsSlice'
import storeMiddlewareConfig from './config/storeMiddlewareConfig'
import { PersistPartial } from 'redux-persist/lib/persistReducer'
import { persistStore, persistReducer } from 'redux-persist'

interface RootState {
  settings: SettingsProps & PersistPartial
}

const persistedReducers = {
  settings: persistReducer<SettingsProps>(settingsPersistConfig, settingsReducer),
}

const nonPersistedReducers = {
  // TODO: Once a reducer, which should not be stored on disk, is created add it here.
  // Example:
  // videos: videosReducer,
}

const rootReducer = combineReducers({
  ...persistedReducers,
  ...nonPersistedReducers,
}) as Reducer<RootState>

const persistedRootReducer = persistReducer<RootState>(rootPersistConfig, rootReducer)

const store = configureStore({
  reducer: persistedRootReducer,
  middleware: (getDefaultMiddleware) => getDefaultMiddleware(storeMiddlewareConfig),
})

const persistor = persistStore(store)

export type AppDispatch = typeof store.dispatch

export { store, persistor, RootState }
