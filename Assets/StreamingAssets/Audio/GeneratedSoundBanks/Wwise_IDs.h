/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID DEATH = 779278001U;
        static const AkUniqueID DYING = 3328495488U;
        static const AkUniqueID FOOTSTEPS = 2385628198U;
        static const AkUniqueID FOOTSTEPS_ENEMY = 550365015U;
        static const AkUniqueID GATE_LOCKED = 1589953441U;
        static const AkUniqueID GATE_OPEN = 3328279379U;
        static const AkUniqueID ITEM = 1222624712U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PORTAL = 3118032615U;
        static const AkUniqueID SHADOW_IN = 4000322109U;
        static const AkUniqueID SHADOW_OUT = 2088474212U;
        static const AkUniqueID STONE_PUSH = 4127039931U;
        static const AkUniqueID TURN = 3137665780U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace LIFE
        {
            static const AkUniqueID GROUP = 2137943U;

            namespace STATE
            {
                static const AkUniqueID DANGER = 4174463524U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SAFE = 938058686U;
            } // namespace STATE
        } // namespace LIFE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace FOOTSTEPS
        {
            static const AkUniqueID GROUP = 2385628198U;

            namespace SWITCH
            {
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace FOOTSTEPS

        namespace FOOTSTEPS_ENEMY
        {
            static const AkUniqueID GROUP = 550365015U;

            namespace SWITCH
            {
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace FOOTSTEPS_ENEMY

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID MASTERVOLUME = 2918011349U;
        static const AkUniqueID MUSICVOLUME = 2346531308U;
        static const AkUniqueID SFXVOLUME = 988953028U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSICBUS = 2886307548U;
        static const AkUniqueID SFXBUS = 3803850708U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
