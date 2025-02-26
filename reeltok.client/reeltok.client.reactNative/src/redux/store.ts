import { rootPersistConfig, settingsPersistConfig } from './config/persistConfig'
import { configureStore, combineReducers, Reducer } from '@reduxjs/toolkit'
import settingsReducer, { SettingsProps } from './slices/settingsSlice'
import commentsReducer, { CommentsProps } from './slices/commentsSlice'
import storeMiddlewareConfig from './config/storeMiddlewareConfig'
import videosReducer, { VideosProps } from './slices/videosSlice'
import usersReducer, { UsersProps } from './slices/usersSlice'
import { PersistPartial } from 'redux-persist/lib/persistReducer'
import { persistStore, persistReducer } from 'redux-persist'

interface RootState {
  settings: SettingsProps & PersistPartial
  comments: CommentsProps
  videos: VideosProps
  users: UsersProps
}

const persistedReducers = {
  settings: persistReducer<SettingsProps>(settingsPersistConfig, settingsReducer),
}

const nonPersistedReducers = {
  comments: commentsReducer,
  videos: videosReducer,
  users: usersReducer,
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
