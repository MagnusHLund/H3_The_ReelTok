import { TypedUseSelectorHook, useSelector } from 'react-redux'
import { RootState } from '../redux/store'

// TODO: Export default
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector
