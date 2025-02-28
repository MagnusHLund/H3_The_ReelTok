import { useWindowDimensions } from 'react-native'
import { useMemo } from 'react'

// TODO: This can be more efficient, if we use useMemo, to not recalculate the heights
export default function useAppDimensions() {
  const { height } = useWindowDimensions()

  const navbarHeight = Math.ceil(height * 0.1)
  const contentHeight = height - navbarHeight

  return { navbarHeight, contentHeight }
}
