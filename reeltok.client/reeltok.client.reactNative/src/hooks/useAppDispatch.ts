import { AppDispatch } from '../redux/store'
import { useDispatch } from 'react-redux'

// TODO: Export default
export const useAppDispatch = () => useDispatch<AppDispatch>()
